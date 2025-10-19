import {Directive, EmbeddedViewRef, Input, OnDestroy, Renderer2, TemplateRef, ViewContainerRef} from '@angular/core';
import {isObservable, Observable, Subscription} from 'rxjs';

@Directive({
  selector: '[appLoading]',
  standalone: false,
})
export class LoadingDirective implements OnDestroy {
  private embeddedViewRef?: EmbeddedViewRef<any>;
  private spinnerElement?: HTMLElement;
  private loadingSub?: Subscription;
  private readonly loadingTemplate = `
    <div id="loading-overlay" class="loading-overlay">
      <div class="google-loader">
        <div class="dot dot1"></div>
        <div class="dot dot2"></div>
        <div class="dot dot3"></div>
        <div class="dot dot4"></div>
      </div>
    </div>
  `;

  constructor(
    private templateRef: TemplateRef<any>,
    private vcr: ViewContainerRef,
    private renderer: Renderer2
  ) {
    // Render the content
    this.embeddedViewRef = this.vcr.createEmbeddedView(this.templateRef);
    // Make sure parent is relatively positioned
    this.setRelativePosition();
  }

  @Input()
  set appLoading(value: boolean | Observable<boolean>) {
    this.clearSubscription();

    if (isObservable(value)) {
      this.loadingSub = value.subscribe((isLoading) =>
        this.toggleSpinner(isLoading)
      );
    } else {
      this.toggleSpinner(value);
    }
  }

  private toggleSpinner(isLoading: boolean) {
    console.log('toggleSpinner', isLoading)
    if (isLoading) {
      this.showSpinner();
    } else {
      this.hideSpinner();
    }
  }

  private showSpinner() {
    console.log('showSpinner', this.spinnerElement);
    if (!this.spinnerElement) {
      this.spinnerElement = this.renderer.createElement('div');
      this.renderer.addClass(this.spinnerElement, 'loading-spinner');
      this.renderer.setProperty(
        this.spinnerElement,
        'innerHTML',
        this.loadingTemplate
      );
      const parent =
        this.embeddedViewRef?.rootNodes[0]?.parentElement;
      console.log('showSpinner parent', parent);
      if (parent) {
        this.renderer.appendChild(parent, this.spinnerElement);
      }
    }
  }

  private hideSpinner() {
    if (this.spinnerElement) {
      const parent =
        this.embeddedViewRef?.rootNodes[0]?.parentElement;
      if (parent) {
        this.renderer.removeChild(parent, this.spinnerElement);
      }
      this.spinnerElement = undefined;
    }
  }

  private setRelativePosition() {
    const parent =
      this.embeddedViewRef?.rootNodes[0]?.parentElement;
    if (parent && getComputedStyle(parent).position === 'static') {
      this.renderer.setStyle(parent, 'position', 'relative');
    }
  }

  private clearSubscription() {
    if (this.loadingSub) {
      this.loadingSub.unsubscribe();
      this.loadingSub = undefined;
    }
  }

  ngOnDestroy(): void {
    this.clearSubscription();
  }
}
