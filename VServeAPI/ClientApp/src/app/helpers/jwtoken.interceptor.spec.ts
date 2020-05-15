import { TestBed } from '@angular/core/testing';

import { JwtokenInterceptor } from './jwtoken.interceptor';

describe('JwtokenInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      JwtokenInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: JwtokenInterceptor = TestBed.inject(JwtokenInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
