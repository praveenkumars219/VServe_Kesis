import { PaymentsPipe } from './payments.pipe';

describe('PaymentsPipe', () => {
  it('create an instance', () => {
    const pipe = new PaymentsPipe();
    expect(pipe).toBeTruthy();
  });
});
