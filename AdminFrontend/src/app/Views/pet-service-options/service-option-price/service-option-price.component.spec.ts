import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceOptionPriceComponent } from './service-option-price.component';

describe('ServiceOptionPriceComponent', () => {
  let component: ServiceOptionPriceComponent;
  let fixture: ComponentFixture<ServiceOptionPriceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceOptionPriceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServiceOptionPriceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
