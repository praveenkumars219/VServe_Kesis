import {
  Component,
  OnInit,
  ViewChild,
  AfterViewInit,
  OnDestroy
} from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators
} from '@angular/forms';
import {
  ActivatedRoute,
  Router
} from '@angular/router';
import {
  switchMap,
  debounceTime,
  tap,
  finalize,
  map,
  catchError,
  startWith
} from 'rxjs/operators';
import {
  UserService
} from 'src/app/services/user.service';
import {
  User
} from 'src/app/models/User';
import {
  AppcommonService
} from 'src/app/services/appcommon.service';
import {
  observable,
  Observable,
  throwError,
  merge,
  of
} from 'rxjs';
import {
  jobcode
} from 'src/app/models/jobcode';
import {
  AuthService
} from 'src/app/services/auth.service';
import {
  location
} from 'src/app/models/location';
import {
  JobdetailService
} from 'src/app/services/jobdetail.service';
import {
  jobdetail
} from 'src/app/models/jobdetail';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { isNullOrUndefined } from 'util';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-jobpost',
  templateUrl: './jobpost.component.html',
  styleUrls: ['./jobpost.component.css']
})
export class JobpostComponent implements OnInit, AfterViewInit, OnDestroy {
  jobPostForm: FormGroup;
  worktypes;
  locations;
  isLoading: boolean;
  isTableLoading = true;
  resultsLength = 0;

  displayedColumns: string[] = ['title', 'location', 'description', 'phone', 'email', 'options'];
  userWorkDetails: MatTableDataSource<jobdetail>;
  userData: jobdetail[];

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private jobService: AppcommonService,
    private authService: AuthService,
    private jobDetails: JobdetailService
  )
  {
  }

  ngOnDestroy(): void {
    this.paginator.page.unsubscribe();
  }

  ngOnInit(): void {
    this.jobPostForm = this.formBuilder.group({
      worktype: [null, Validators.required],
      location: [null, Validators.required],
      description: null,
      details: null,
      email: ['', Validators.email],
      phone: ['', Validators.required],
      id: null,
      startDate: null,
    });

    this.jobPostForm
      .get('worktype')
      .valueChanges
      .pipe(
        debounceTime(300),
        tap(() => this.isLoading = true),
        switchMap(value => this.getfilteredJobService(value)
          .pipe(
            finalize(() => this.isLoading = false),
          )
        )
      )
      .subscribe((locations: []) => {
        this.worktypes = locations;
      });

    this.jobPostForm
      .get('location')
      .valueChanges
      .pipe(
        debounceTime(300),
        tap(() => this.isLoading = true),
        switchMap(value => this.jobService.searchLocations(value)
          .pipe(
            finalize(() => this.isLoading = false),
          )
        )
      )
      .subscribe((worktype) => {
        this.locations = worktype;
      });

  }

  ngAfterViewInit() {
    this.getJobDetails();
  }

  get f() {
    return this.jobPostForm.controls;
  }

  onSubmit() {
    if (this.jobPostForm.invalid) {
      return;
    }

    const updateJob: jobdetail = {
      id: this.f.id.value,
      email: this.f.email.value,
      phoneNo: this.f.phone.value,
      jobProfile: this.f.worktype.value,
      location: this.f.location.value,
      jobDescription: this.f.description.value,
      additionalDetails: this.f.details.value,
      userId: this.authService.currentUserV.userId,
      startDate: isNullOrUndefined(this.f.id.value) ?
      new DatePipe('en-US').transform(Date.now(), 'MM/dd/yyyy') : this.f.startDate.value,
      isValid: true,
    };

    if (isNullOrUndefined(updateJob.id)) {
      this.save(updateJob);
    } else {
      this.update(updateJob);
    }

  }

  save(updateJob) {
    this.jobDetails.saveJobDetails(updateJob)
      .subscribe(response => {
        if (response === true) {
          this.getJobDetails();
          this.jobPostForm.reset();
          this.jobPostForm.markAsUntouched();
        }
      }, error => {
      });
  }

  update(updateJob) {
    this.jobDetails.updateJobDetails(updateJob.id, updateJob)
      .subscribe(response => {
        if (response === true) {
          this.getJobDetails();
          this.jobPostForm.reset();
          this.jobPostForm.markAsUntouched();
        }
      }, error => {
      });
  }


  displayJob(job: jobcode): string {
    return job ? job.jobTitle : '';
  }

  displayLoc(loc: location): string {
    return loc ? loc.locationName : '';
  }

  getfilteredJobService(value): Observable < jobcode[] > {
    return this.jobService.searchJobType(value).pipe(
      map(response => {
        return response.filter(x => x.jobTitle.includes(value));
      }),
      catchError(err => {
        return throwError(err);
      }));
  }

  getJobDetails() {
    this.jobDetails.getJobDetails(this.authService.currentUserV.userId).pipe(
      map(response => response,
      catchError(err => {
        return throwError(err);
      })
    )).subscribe(data =>
      {
      this.userWorkDetails = new MatTableDataSource(data.filter(x => x.isValid === true));
      this.userWorkDetails.paginator = this.paginator ;
      this.userWorkDetails.sort = this.sort;
      this.isTableLoading = false;
    } );
  }

  editwork(work) {
    this.jobPostForm.patchValue({
      id: work.id,
      worktype: {
        id: work.jobProfile.id,
        jobCode: work.jobProfile.jobCode,
        JobTitle: work.jobProfile.jobTitle
      },
      location: work.location,
      email: work.email,
      phone: work.phoneNo,
      description : work.jobDescription,
      details: work.additionalDetails
    });
  }

  deletework(work) {
    work.IsValid = false;
    work.IsValidEndDate =  new DatePipe('en-US').transform(Date.now(), 'MM/dd/yyyy'),
    this.update(work);
  }

}
