import {
  Component,
  OnInit,
  ViewChild
} from '@angular/core';
import {
  UserService
} from 'src/app/services/user.service';
import {
  AuthService
} from 'src/app/services/auth.service';
import {
  User
} from 'src/app/models/User';
import {
  UserDetail
} from 'src/app/models/UserDetail';
import { FormGroup, FormBuilder } from '@angular/forms';
import { isNullOrUndefined } from 'util';
import { DatePipe } from '@angular/common';
import { AssignmentsService } from 'src/app/services/assignments.service';
import { Assignment } from 'src/app/models/assignment';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatRadioChange } from '@angular/material/radio';
import { Payment } from 'src/app/models/payment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  user: UserDetail;
  logUser: User;
  profileImg: File;
  profileImgSrc: string | ArrayBuffer;
  profileForm: FormGroup;
  userLoaded = false;
  assignments: Assignment[] = [];
  displayedColumns: string[] = ['email', 'location', 'description', 'phone', 'title'];
  status: string[] = ['Applied', 'In Progress', 'Completed', 'All'];
  filteredStatus: any;
  isTableLoading = false;
  payments: Payment[] = [];


  userAssignments: MatTableDataSource<Assignment>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private userService: UserService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private assignmentservice: AssignmentsService) {}

  ngOnInit(): void {
    this.logUser = this.authService.currentUserV;
    this.profileForm = this.formBuilder.group({
      id: [null],
      firstname: [null],
      gender: [null],
      email: [null],
      location: [null],
      lastname: [null],
      date: [null],
      phone: [null],
      profession: [null],
      description: [null]
    });

    this.profileForm.disable();

    this.userService.getuserFromDb(this.authService.currentUserV.userId)
      .subscribe(x => {
        this.user = x;
        this.userLoaded = true;
        if (x !== null && x !== undefined) {
          this.profileForm = this.formBuilder.group({
            id: x.id,
            firstname: x.firstName,
            gender: x.gender,
            email: this.authService.currentUserV.email,
            location: x.location,
            lastname: x.lastName,
            date:  new Date(x.dateofBirth).toISOString().substring(0, 10),
            phone: x.phone,
            profession: x.profession,
            description: x.description
          });
        }
      }, error => console.log(error));

      this.getAssignments();
  }

  get f() {
    return this.profileForm.controls;
  }

  onSubmit() {
    if (this.profileForm.invalid) {
      return;
    }

    const updateUser: UserDetail = {
      userId: this.authService.currentUserV.userId,
      id: this.f.id.value,
      firstName: this.f.firstname.value,
      lastName: this.f.lastname.value,
      dateofBirth: new Date(this.datePipe.transform(this.f.date.value, 'MM-dd-yyyy')),
      gender: this.f.gender.value,
      location: this.f.location.value,
      profession: this.f.profession.value,
      email: this.f.email.value,
      phone: this.f.phone.value,
      description: this.f.description.value
    };

    this.userService.updateUserDetails(updateUser).subscribe(x => x);
  }

  edit() {
    this.profileForm.enable();
  }

  getAssignments() {
    this.assignmentservice.getAssignmentDetails(this.authService.currentUserV.userId)
      .subscribe(response => { this.assignments = response;
        this.userAssignments = new MatTableDataSource(response);
        this.userAssignments.paginator = this.paginator;
        this.userAssignments.sort = this.sort;
      });
  }

  fileSelected(event) {
    this.profileImg = <File>(event).target.files[0];
    this.readFile();
  }

  uploadFile() {

  }

  readFile() {
    const reader = new FileReader();
        reader.onload = e => this.profileImgSrc = reader.result;
        reader.readAsDataURL(this.profileImg);
  }



  filterChange(event: MatRadioChange) {
    this.userAssignments.filterPredicate = (data: any, filter: string) => data.workStatus.toString() === filter;

    switch (event.value) {
      case this.status[0]:
        this.userAssignments.filter = '0';
        break;
      case this.status[1]:
        this.userAssignments.filter = '1';
        break;
      case this.status[2]:
        this.userAssignments.filter = '2';
          break;
      case this.status[3]:
      default:
        this.userAssignments.filter = null;
        break;
    }
  }

}
