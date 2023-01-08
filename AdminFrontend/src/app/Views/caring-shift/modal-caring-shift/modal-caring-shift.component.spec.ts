import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalCaringShiftComponent } from './modal-caring-shift.component';

describe('ModalCaringShiftComponent', () => {
  let component: ModalCaringShiftComponent;
  let fixture: ComponentFixture<ModalCaringShiftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalCaringShiftComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalCaringShiftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
