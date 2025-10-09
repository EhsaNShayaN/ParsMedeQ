import {Component, ElementRef, Inject, Input, OnInit, ViewChild} from '@angular/core';
import {PureComponent} from '../../pure-component';
import {HttpEvent, HttpEventType} from '@angular/common/http';
import {DomSanitizer} from '@angular/platform-browser';
import {DOCUMENT} from "@angular/common";

@Component({
  selector: 'app-download-management',
  templateUrl: './download-management.component.html',
  styleUrls: ['./download-management.component.scss']
})
export class DownloadManagementComponent extends PureComponent implements OnInit {
  @Input() id: string;
  @Input() price: number;
  @Input() tableId: number;
  @Input() icon: string;
  @Input() model: string = null;
  @Input() buttonTitle = 'DOWNLOAD';
  @ViewChild('hiddenA') hiddenARef: ElementRef | undefined;

  isLoaded = false;
  isDownloading = false;
  url = '';
  blob: any;
  fileUrl: any;
  downloadName: any;
  downloadProgress: number | undefined;

  constructor(@Inject(DOCUMENT) private document: Document,
              private sanitizer: DomSanitizer) {
    super();
  }

  ngOnInit() {
    this.buttonTitle = this.appService.getTranslateValue(this.buttonTitle);
  }

  download0() {
    this.restClientService.download(this.id, this.tableId, this.model).subscribe((blob: Blob) => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = "rtl-sample.pdf"; // suggested filename
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }

  download() {
    console.log('download123');
    this.onOpenFile();
  }

  onOpenFile() {
    if (this.isLoaded && this.blob) {
      const data = this.blob;
      const blob = new Blob([data], {type: 'application/octet-stream'});
      const q = window.URL.createObjectURL(blob);
      this.fileUrl = this.sanitizer.bypassSecurityTrustResourceUrl(q);
      setTimeout(() => {
        this.openFile();
      }, 0);
      return;
    }

    this.isDownloading = true;
    this.restClientService.download(this.id, this.tableId, this.model).subscribe((event: HttpEvent<any>) => {
      console.log('download', event);
      if (event.type === HttpEventType.ResponseHeader) {
        const contentDisposition = event.headers.get('Content-Disposition');
        if (contentDisposition) {
          const match = contentDisposition.match(/filename\*=UTF-8''(.+)/);
          if (match && match[1]) {
            this.downloadName = decodeURIComponent(match[1]);
          } else {
            this.downloadName = 'downloaded_file';
          }
        }
      }
      if (event.type === HttpEventType.DownloadProgress) {
        if (event.total) {
          this.downloadProgress = event.loaded / event.total;
        } else {
          this.downloadProgress = -1;
        }
      }
      if (event.type === HttpEventType.Response) {
        this.isDownloading = false;
        this.blob = event.body;
        this.isLoaded = true;
        setTimeout(() => {
          this.onOpenFile();
        }, 0);
      }
    });
  }

  openFile() {
    const a = this.hiddenARef?.nativeElement;
    if (a) {
      a.click();
      window.URL.revokeObjectURL(this.url);
    }
  }
}
