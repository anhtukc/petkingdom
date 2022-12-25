import { TestBed } from '@angular/core/testing';

import { ApiScheduleAvailableService } from './api-schedule-available.service';

describe('ApiScheduleAvailableService', () => {
  let service: ApiScheduleAvailableService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiScheduleAvailableService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
