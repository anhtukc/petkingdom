import { TestBed } from '@angular/core/testing';

import { ServiceOptionService } from './service-option.service';

describe('ServiceOptionService', () => {
  let service: ServiceOptionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceOptionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
