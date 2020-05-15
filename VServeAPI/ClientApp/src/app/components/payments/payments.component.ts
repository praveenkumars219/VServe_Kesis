import { Component, OnInit, Input } from '@angular/core';
import { Assignment } from 'src/app/models/assignment';
import { AssignmentsService } from 'src/app/services/assignments.service';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { UserDetail } from 'src/app/models/UserDetail';
import { User } from 'src/app/models/User';
import { NgForm } from '@angular/forms';
import { Payment } from 'src/app/models/payment';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { DatePipe } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PaymentsPipe } from 'src/app/pipes/payments.pipe';
import { PayeefilterPipe } from 'src/app/pipes/payeefilter.pipe';

@Component({
  selector: 'app-payments',
  providers: [PaymentsPipe, PayeefilterPipe],
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.css']
})
export class PaymentsComponent implements OnInit {

  @Input() assignments: Assignment[];
  paymentypes: string[] = ['Credit Card', 'Debit Card', 'Cash'];
  paymentype;
  assignmentUser: User;
  valuechanged;
  assignedAmount;
  payment: Payment;
  selectedAssignment: Assignment;
  pickerdate;
  payments: Payment[];
  currentUser: string;

  constructor(private assignmentservice: AssignmentsService,
    private authService: AuthService,
    private userService: UserService,
    private _snackBar: MatSnackBar,
    private paymentfilter: PaymentsPipe,
    private payeefilter: PayeefilterPipe) {
  }

  ngOnInit(): void {
    this.getPayments();
    this.currentUser = this.authService.currentUserV.userId;
  }

  getPayments() {
    this.assignmentservice.getPaymentDetails(this.authService.currentUserV.userId)
      .subscribe(response => {
        this.payments = response;
      }, error => { }
      );
  }

  assignmentchanged(event, assignment: Assignment) {
    this.selectedAssignment = assignment;
    this.userService.getuser(assignment.assignedUserId)
      .subscribe(response =>
        this.assignmentUser = response
      );
  }

  onSubmit(f: NgForm) {
    if (!f.valid) {
      return;
    }

    const payObject: Payment = {
      id: null,
      assignmentId: this.selectedAssignment.id,
      paymentDate: this.pickerdate,
      payee: this.selectedAssignment.assignedJob.userId,
      payer: this.selectedAssignment.assignedUserId,
      paymentAmount: f.value.amount
    };

    this.assignmentservice.savePayments(payObject).subscribe(
      response => {
        const message = response === true ? 'Payment Added Successfully' : 'Payment Failed to Add';
        this._snackBar.open(message, payObject.paymentAmount.toString(), {
          duration: 2000,
        });
      },
      error => console.log(error)
    );
  }

  dateEvent(event: MatDatepickerInputEvent<Date>) {
    this.pickerdate = new DatePipe('en-US').transform(event.value, 'MM/dd/yyyy');
    console.log(this.pickerdate);

  }

}
