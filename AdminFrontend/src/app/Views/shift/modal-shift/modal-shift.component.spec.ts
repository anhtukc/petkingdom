import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalShiftComponent } from './modal-shift.component';

describe('ModalShiftComponent', () => {
  let component: ModalShiftComponent;
  let fixture: ComponentFixture<ModalShiftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalShiftComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalShiftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
