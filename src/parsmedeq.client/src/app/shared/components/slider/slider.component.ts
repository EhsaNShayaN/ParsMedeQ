import {Component, Input} from '@angular/core';

@Component({
    selector: 'app-slider',
    templateUrl: './slider.component.html',
    styleUrls: ['./slider.component.scss'],
    standalone: false
})
export class SliderComponent {
    @Input() slides: any[] = [];
    @Input() rtl: boolean = true;
}
