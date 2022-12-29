import { TestBed } from '@angular/core/testing';

import { ApiServiceOptionPriceService } from './api-service-option-price.service';

describe('ApiServiceOptionPriceService', () => {
  let service: ApiServiceOptionPriceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiServiceOptionPriceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
