import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalServiceOptionComponent } from './modal-service-option.component';

describe('ModalServiceOptionComponent', () => {
  let component: ModalServiceOptionComponent;
  let fixture: ComponentFixture<ModalServiceOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalServiceOptionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalServiceOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
