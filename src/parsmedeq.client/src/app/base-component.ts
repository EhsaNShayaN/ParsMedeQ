import {Directive, Injector} from '@angular/core';
import {PureComponent} from './pure-component';
import {Meta, Title} from '@angular/platform-browser';

@Directive()
export class BaseComponent extends PureComponent {
  titleService: Title;
  metaService: Meta;

  constructor(injector: Injector) {
    super(injector);
    this.titleService = injector.get(Title);
    this.metaService = injector.get(Meta);
  }

  setTitle(title: string | null = null) {
    if (!title) {
      title = 'AlborzChem | البرز شیمی';
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
