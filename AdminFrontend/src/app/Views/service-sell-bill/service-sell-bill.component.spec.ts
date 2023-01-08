import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceSellBillComponent } from './service-sell-bill.component';

describe('ServiceSellBillComponent', () => {
  let component: ServiceSellBillComponent;
  let fixture: ComponentFixture<ServiceSellBillComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceSellBillComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServiceSellBillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
