import { TestBed } from '@angular/core/testing';

import { AppcommonService } from './appcommon.service';

describe('AppcommonService', () => {
  let service: AppcommonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppcommonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
