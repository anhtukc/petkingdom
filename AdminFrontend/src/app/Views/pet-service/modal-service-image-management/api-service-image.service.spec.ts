import { TestBed } from '@angular/core/testing';

import { ApiServiceImageService } from './api-service-image.service';

describe('ApiServiceImageService', () => {
  let service: ApiServiceImageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiServiceImageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
