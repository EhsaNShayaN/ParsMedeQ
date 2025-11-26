import {ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, Input, OnDestroy, Output} from '@angular/core';
import {interval, Subscription} from 'rxjs';

export interface TimeLeft {
  totalMs: number;
  days: number;
  hours: number;
  minutes: number;
  seconds: number;
  milliseconds: number;
}

@Component({
  selector: 'app-otp-countdown',
  templateUrl: './otp-countdown.component.html',
  styleUrls: ['./otp-countdown.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false
})
export class OtpCountdownComponent implements OnDestroy {
  @Input() set target(value: string | Date | number) {
    this._target = this.toDate(value);
    this.reset();
  }

  @Input() intervalMs = 1000; // tick interval in milliseconds
  @Output() finished = new EventEmitter<void>();
  @Output() tick = new EventEmitter<TimeLeft>();

  private _target!: Date;
  private sub?: Subscription;
  private paused = false;

  public timeLeft: TimeLeft = this.calcTimeLeft(0);

  constructor(private cdr: ChangeDetectorRef) {
  }

  ngOnDestroy(): void {
    this.stopTicker();
  }

  private toDate(v?: string | Date | number): Date {
    if (!v) return new Date();
    if (v instanceof Date) return v;
    return new Date(v);
  }

  private stopTicker() {
    if (this.sub) {
      this.sub.unsubscribe();
      this.sub = undefined;
    }
  }

  start() {
    if (!this._target) return;
    this.stopTicker();
    this.paused = false;

    this.sub = interval(this.intervalMs).subscribe(() => {
      if (this.paused) return;
      const remaining = this._target.getTime() - Date.now();
      this.timeLeft = this.calcTimeLeft(Math.max(0, remaining));
      this.tick.emit(this.timeLeft);
      this.cdr.markForCheck();

      if (remaining <= 0) {
        this.finished.emit();
        this.stopTicker();
      }
    });

    // emit initial immediately
    const initial = Math.max(0, this._target.getTime() - Date.now());
    this.timeLeft = this.calcTimeLeft(initial);
    this.tick.emit(this.timeLeft);
    this.cdr.markForCheck();
  }

  pause() {
    this.paused = true;
  }

  resume() {
    if (!this._target) return;
    this.paused = false;
  }

  reset(newTarget?: string | Date | number) {
    if (newTarget !== undefined) this._target = this.toDate(newTarget);
    this.stopTicker();
    const remaining = Math.max(0, this._target.getTime() - Date.now());
    this.timeLeft = this.calcTimeLeft(remaining);
    this.cdr.markForCheck();
    // start automatically
    this.start();
  }

  private calcTimeLeft(ms: number): TimeLeft {
    const totalMs = ms;
    const milliseconds = ms % 1000;
    const totalSeconds = Math.floor(ms / 1000);
    const seconds = totalSeconds % 60;
    const totalMinutes = Math.floor(totalSeconds / 60);
    const minutes = totalMinutes % 60;
    const totalHours = Math.floor(totalMinutes / 60);
    const hours = totalHours % 24;
    const days = Math.floor(totalHours / 24);

    return {totalMs, days, hours, minutes, seconds, milliseconds};
  }
}
