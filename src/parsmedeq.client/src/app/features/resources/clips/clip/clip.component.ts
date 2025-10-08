import {Component} from '@angular/core';
import {BasePageResource} from '../../base-page-resource';
import {Tables} from '../../../../core/constants/server.constants';
import {DomSanitizer} from '@angular/platform-browser';
import {endpoint} from '../../../../core/services/cookie-utils';

@Component({
  selector: 'app-clip',
  templateUrl: './clip.component.html',
  styleUrls: ['./clip.component.scss'],
  standalone: false,
})
export class ClipComponent extends BasePageResource {
  constructor(private sanitizer: DomSanitizer) {
    super(Tables.Clip);
  }

  getFilePath(videoId?: number) {
    if (!videoId) {
      return null;
    }
    const url = `${endpoint()}general/download?id=${videoId}`;
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);

  }
}
