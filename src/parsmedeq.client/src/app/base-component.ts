import {Directive, inject} from '@angular/core';
import {PureComponent} from './pure-component';
import {Meta, Title} from '@angular/platform-browser';
import {TranslateService} from '@ngx-translate/core';

@Directive()
export class BaseComponent extends PureComponent {
  titleService = inject(Title);
  metaService = inject(Meta);

  constructor() {
    super();
  }

  setTitle(title: string | null = null) {
    if (!title) {
      title = 'ParsMedeQ | پارس مدیکیو';
    }
    this.titleService.setTitle(title);
  }

  private updateMeta(property: string, content: string) {
    this.metaService.updateTag({property, content});
  }

  setMetaDescription(content: string) {
    this.updateMeta('description', content);
  }

  setMetaKeywords(content: string) {
    this.updateMeta('keywords', content);
  }
}
