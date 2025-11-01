import {Component, forwardRef, Injector, Input, OnInit} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl} from '@angular/forms';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';

export interface FileUploadValue {
  existing: { id: number; url: string }[];
  new: File[];
  deleted: number[];
}

interface UnifiedItem {
  type: 'existing' | 'new';
  id?: number;
  value: string | File;
  preview: string;
}

@Component({
  selector: 'app-multi-file-upload',
  templateUrl: './multi-file-upload.component.html',
  styleUrls: ['./multi-file-upload.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MultiFileUploadComponent),
      multi: true,
    },
  ],
  standalone: false,
})
export class MultiFileUploadComponent implements ControlValueAccessor, OnInit {
  //@Input() accept: string = 'image/png, image/jpeg, image/jpg';
  @Input() label: string = '';
  @Input() accept: string = '*/*';
  @Input() maxFiles: number = 1;
  @Input() maxSizeMb: number = 1;
  @Input() maxWidth: number = 1080;
  @Input() maxHeight: number = 1080;
  @Input() aspectRatio: number = -1;

  items: UnifiedItem[] = [];
  deleted: number[] = [];
  isDragOver = false;
  errors: string[] = [];

  onChange = (value: any) => {
  };
  onTouched = () => {
  };
  disabled = false;

  public ngControl: NgControl | null = null;

  constructor(private injector: Injector) {
  }

  ngOnInit(): void {
    // Lazily resolve to avoid circular dep during construction
    this.ngControl = this.injector.get(NgControl, null, {self: true, optional: true});
  }

  writeValue(value: FileUploadValue | null): void {
    this.items = [];
    this.deleted = [];
    this.errors = [];

    if (value) {
      if (Array.isArray(value.existing)) {
        this.items.push(
          ...value.existing.map((img) => ({
            type: 'existing' as const,
            id: img.id,
            value: img.url,
            preview: img.url,
          }))
        );
      }
      if (Array.isArray(value.new)) {
        value.new.forEach((file: File) => {
          if (file instanceof File) {
            this.readFile(file, (result) => {
              this.items.push({
                type: 'new',
                value: file,
                preview: result,
              });
              this.updateModel();
            });
          }
        });
      }
      if (Array.isArray(value.deleted)) {
        this.deleted = [...value.deleted];
      }
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  // File select
  onFileSelected(event: any) {
    const selectedFiles: FileList = event.target.files;
    if (!selectedFiles) return;
    this.handleFiles(Array.from(selectedFiles));
    event.target.value = '';
  }

  // Drag & Drop
  onFileDropped(event: DragEvent) {
    event.preventDefault();
    this.isDragOver = false;
    if (this.disabled) return;
    const files = event.dataTransfer?.files;
    if (files) this.handleFiles(Array.from(files));
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
    this.isDragOver = true;
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    this.isDragOver = false;
  }

  private handleFiles(files: File[]) {
    this.errors = [];
    files.forEach((file) => {
      // Type validation
      if (this.accept !== '*/*' && !this.accept.includes(file.type)) {
        this.errors.push(`${file.name}: invalid type`);
        return;
      }

      // Max count
      if (this.maxFiles !== -1 && this.items.length >= this.maxFiles) {
        this.errors.push(`Cannot select more than ${this.maxFiles} files`);
        return;
      }

      // Max size
      if (this.maxSizeMb !== -1 && file.size > this.maxSizeMb * 1024 * 1024) {
        this.errors.push(`${file.name}: exceeds ${this.maxSizeMb}MB`);
        return;
      }

      // Validate dimension + aspect ratio
      this.validateImage(file).then((isValid) => {
        if (!isValid) return;
        this.readFile(file, (result) => {
          this.items.push({
            type: 'new',
            value: file,
            preview: result,
          });
          this.updateModel();
        });
      });
    });
  }

  private validateImage(file: File): Promise<boolean> {
    return new Promise((resolve) => {
      const img = new Image();
      const url = URL.createObjectURL(file);

      img.onload = () => {
        const {width, height} = img;
        URL.revokeObjectURL(url);

        if ((this.maxWidth !== -1 && width > this.maxWidth) || (this.maxHeight !== -1 && height > this.maxHeight)) {
          this.errors.push(
            `${file.name}: must be a maximum of ${this.maxWidth}x${this.maxHeight}px`
          );
          return resolve(false);
        }

        if (this.aspectRatio !== -1) {
          const ratio = width / height;
          const diff = Math.abs(ratio - this.aspectRatio);
          if (diff > 0.01) {
            this.errors.push(
              `${file.name}: must have aspect ratio ${this.aspectRatio.toFixed(2)}`
            );
            return resolve(false);
          }
        }

        resolve(true);
      };

      img.onerror = () => {
        this.errors.push(`${file.name}: could not read image`);
        resolve(false);
      };

      img.src = url;
    });
  }

  private readFile(file: File, callback: (res: string) => void) {
    const reader = new FileReader();
    reader.onload = (e: any) => callback(e.target.result);
    reader.readAsDataURL(file);
  }

  removeFile(index: number) {
    const removed = this.items.splice(index, 1)[0];
    if (removed.type === 'existing' && removed.id != null) {
      this.deleted.push(removed.id);
    }
    this.updateModel();
  }

  drop(event: CdkDragDrop<UnifiedItem[]>) {
    moveItemInArray(this.items, event.previousIndex, event.currentIndex);
    this.updateModel();
  }

  private updateModel() {
    const existing: { id: number; url: string }[] = [];
    const newFiles: File[] = [];

    this.items.forEach((item) => {
      if (item.type === 'existing') {
        existing.push({id: item.id!, url: item.value as string});
      } else {
        newFiles.push(item.value as File);
      }
    });

    this.onChange({existing, new: newFiles, deleted: this.deleted});
  }

  get control() {
    return this.ngControl?.control;
  }
}
