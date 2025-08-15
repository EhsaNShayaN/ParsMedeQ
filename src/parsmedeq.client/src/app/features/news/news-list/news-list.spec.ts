import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsList } from './news-list';

describe('News', () => {
  let component: NewsList;
  let fixture: ComponentFixture<NewsList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NewsList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewsList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
