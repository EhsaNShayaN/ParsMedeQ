import {AfterViewInit, Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-not-acceptable',
  templateUrl: './not-acceptable.html',
  styleUrls: ['./not-acceptable.scss'],
  standalone: false
})
export class NotAcceptableComponent implements OnInit, AfterViewInit {

  constructor(public router: Router) {
  }

  ngOnInit() {
  }

  public goHome(): void {
    this.router.navigate(['/']);
  }

  ngAfterViewInit() {
    document.getElementById('preloader')?.classList.add('hide');
  }
}
