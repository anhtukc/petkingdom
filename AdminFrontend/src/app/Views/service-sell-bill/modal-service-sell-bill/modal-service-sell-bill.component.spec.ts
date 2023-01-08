import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalServiceSellBillComponent } from './modal-service-sell-bill.component';

describe('ModalServiceSellBillComponent', () => {
  let component: ModalServiceSellBillComponent;
  let fixture: ComponentFixture<ModalServiceSellBillComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalServiceSellBillComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalServiceSellBillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
