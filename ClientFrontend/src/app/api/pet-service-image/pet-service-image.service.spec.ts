import { TestBed } from '@angular/core/testing';

import { PetServiceImageService } from './pet-service-image.service';

describe('PetServiceImageService', () => {
  let service: PetServiceImageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PetServiceImageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
