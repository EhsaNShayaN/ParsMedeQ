import {Directive} from '@angular/core';
import {PureComponent} from '../../../../pure-component';
import {MainSections} from '../../../../core/constants/server.constants';

@Directive()
export class BaseSectionDialog extends PureComponent {
  sectionTitle: string = '';
  mainSections= MainSections;

  constructor() {
    super();
  }
}
