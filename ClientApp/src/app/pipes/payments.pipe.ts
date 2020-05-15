import { Pipe, PipeTransform, Injectable } from '@angular/core';
import { Payment } from '../models/payment';

@Pipe({
  name: 'paymentfilter'
})

@Injectable()
export class PaymentsPipe implements PipeTransform {

  transform(payments: Payment[], args: any[]): any {
    return payments?.filter(pay =>
      pay.payer === args[0]
      );
  }

}
