import { TestBed } from '@angular/core/testing';

import { PetServiceApiService } from './pet-service-api.service';

describe('PetServiceApiService', () => {
  let service: PetServiceApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PetServiceApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
