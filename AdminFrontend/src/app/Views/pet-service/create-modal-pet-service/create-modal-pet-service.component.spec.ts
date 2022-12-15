import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateModalPetServiceComponent } from './create-modal-pet-service.component';

describe('CreateModalPetServiceComponent', () => {
  let component: CreateModalPetServiceComponent;
  let fixture: ComponentFixture<CreateModalPetServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateModalPetServiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateModalPetServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
