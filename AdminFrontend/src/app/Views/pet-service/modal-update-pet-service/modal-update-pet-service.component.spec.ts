import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalUpdatePetServiceComponent } from './modal-update-pet-service.component';

describe('ModalUpdatePetServiceComponent', () => {
  let component: ModalUpdatePetServiceComponent;
  let fixture: ComponentFixture<ModalUpdatePetServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalUpdatePetServiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalUpdatePetServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
