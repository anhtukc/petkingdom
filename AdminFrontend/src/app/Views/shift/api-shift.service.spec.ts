import { TestBed } from '@angular/core/testing';

import { ApiShiftService } from './api-shift.service';

describe('ApiShiftService', () => {
  let service: ApiShiftService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiShiftService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
