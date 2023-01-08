import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CaringShiftComponent } from './caring-shift.component';

describe('CaringShiftComponent', () => {
  let component: CaringShiftComponent;
  let fixture: ComponentFixture<CaringShiftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CaringShiftComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CaringShiftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
