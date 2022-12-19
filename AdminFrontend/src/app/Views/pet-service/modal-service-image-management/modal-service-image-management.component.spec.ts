import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalServiceImageManagementComponent } from './modal-service-image-management.component';

describe('ModalServiceImageManagementComponent', () => {
  let component: ModalServiceImageManagementComponent;
  let fixture: ComponentFixture<ModalServiceImageManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalServiceImageManagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalServiceImageManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
