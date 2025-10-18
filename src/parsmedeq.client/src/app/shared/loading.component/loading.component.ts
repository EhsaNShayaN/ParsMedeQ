import {Component, Input} from '@angular/core';
import {CommonModule} from '@angular/common';
import {BehaviorSubject} from 'rxjs';

@Component({
  selector: 'app-loading',
  imports: [CommonModule],
  templateUrl: './loading.component.html',
  styleUrl: './loading.component.scss',
  standalone: true
})
export class LoadingComponent {
  loading$ = new BehaviorSubject<boolean>(true);

  @Input() l: BehaviorSubject<boolean> | boolean = false;
}
