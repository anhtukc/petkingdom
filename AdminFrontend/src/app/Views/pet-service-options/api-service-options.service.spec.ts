import { TestBed } from '@angular/core/testing';

import { ApiServiceOptionsService } from './api-service-options.service';

describe('ApiServiceOptionsService', () => {
  let service: ApiServiceOptionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiServiceOptionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
