import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RepairRequest } from './repair-request';

describe('RepairRequest', () => {
  let component: RepairRequest;
  let fixture: ComponentFixture<RepairRequest>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RepairRequest]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RepairRequest);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
