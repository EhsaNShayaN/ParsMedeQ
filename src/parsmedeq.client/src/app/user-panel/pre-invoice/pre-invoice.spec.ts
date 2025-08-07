import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreInvoice } from './pre-invoice';

describe('PreInvoice', () => {
  let component: PreInvoice;
  let fixture: ComponentFixture<PreInvoice>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PreInvoice]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PreInvoice);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
