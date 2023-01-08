import { TestBed } from '@angular/core/testing';

import { ApiSellbillService } from './api-sellbill.service';

describe('ApiSellbillService', () => {
  let service: ApiSellbillService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiSellbillService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
