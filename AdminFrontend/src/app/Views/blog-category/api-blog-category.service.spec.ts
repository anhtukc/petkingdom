import { TestBed } from '@angular/core/testing';

import { ApiBlogCategoryService } from './api-blog-category.service';

describe('ApiBlogCategoryService', () => {
  let service: ApiBlogCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiBlogCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
