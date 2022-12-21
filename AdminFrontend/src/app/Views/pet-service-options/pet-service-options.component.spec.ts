import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetServiceOptionsComponent } from './pet-service-options.component';

describe('PetServiceOptionsComponent', () => {
  let component: PetServiceOptionsComponent;
  let fixture: ComponentFixture<PetServiceOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PetServiceOptionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PetServiceOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
