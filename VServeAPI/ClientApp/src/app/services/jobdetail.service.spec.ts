import { TestBed } from '@angular/core/testing';

import { JobdetailService } from './jobdetail.service';

describe('JobdetailService', () => {
  let service: JobdetailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JobdetailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
