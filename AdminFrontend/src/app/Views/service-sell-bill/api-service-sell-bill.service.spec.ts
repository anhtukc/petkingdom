import { TestBed } from '@angular/core/testing';

import { ApiServiceSellBillService } from './api-service-sell-bill.service';

describe('ApiServiceSellBillService', () => {
  let service: ApiServiceSellBillService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiServiceSellBillService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
