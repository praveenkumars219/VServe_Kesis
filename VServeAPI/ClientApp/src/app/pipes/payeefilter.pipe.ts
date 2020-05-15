import { Pipe, PipeTransform, Injectable } from '@angular/core';
import { Payment } from '../models/payment';

@Pipe({
  name: 'payeefilter'
})

@Injectable()
export class PayeefilterPipe implements PipeTransform {

  transform(payments: Payment[], args: any[]): any {
    return payments?.filter(pay =>
      pay.payee === args[0]
      );
  }

}
