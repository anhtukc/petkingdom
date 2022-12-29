import { TestBed } from '@angular/core/testing';

import { ApiPetService } from './pet-service.service';

describe('ApiPetService', () => {
  let service: ApiPetService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiPetService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
