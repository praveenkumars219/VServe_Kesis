import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppcommonService } from 'src/app/services/appcommon.service';
import { AuthService } from 'src/app/services/auth.service';
import { JobdetailService } from 'src/app/services/jobdetail.service';
import { debounceTime, tap, switchMap, finalize, map, catchError } from 'rxjs/operators';
import { Observable, throwError, config } from 'rxjs';
import { jobcode } from 'src/app/models/jobcode';
import { location } from 'src/app/models/location';
import { jobdetail } from 'src/app/models/jobdetail';
import { AssignmentsService } from 'src/app/services/assignments.service';
import { Assignment } from 'src/app/models/assignment';
import { MatSnackBar } from '@angular/material/snack-bar';
import { JobtitlefilterPipe } from 'src/app/pipes/jobtitlefilter.pipe';

@Component({
  selector: 'app-jobsearch',
  providers: [JobtitlefilterPipe],
  templateUrl: './jobsearch.component.html',
  styleUrls: ['./jobsearch.component.css']
})
export class JobsearchComponent implements OnInit {

  jobPostForm: FormGroup;
  worktypes: jobcode[];
  locations: location[];
  isLoading: boolean;
  isTableLoading = true;
  resultsLength = 0;
  jobsResponse: jobdetail[] = [];
  assignments: Assignment[] = [];
  issuccess: string;
  InProgress: any;
  Applied: any;
  Completed: any;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private jobService: AppcommonService,
    private authService: AuthService,
    private jobDetails: JobdetailService,
    private assignmentservice: AssignmentsService,
    private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.jobPostForm = this.formBuilder.group({
      worktype: [null, Validators.required],
      location: [null, Validators.required]
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
      .subscribe((works: []) => {
        this.worktypes = works;
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
      .subscribe((locs) => {
        this.locations = locs;
      });
  }

  saveAssignment(jobToAssign: jobdetail){
    const assignmentToSend: Assignment = {
      id: null,
      assignedJob: jobToAssign,
      workStatus: 0,
      assignedUserId: this.authService.currentUserV.userId
    };

    this.assignmentservice.saveAssignmentDetails(assignmentToSend)
      .subscribe(response => {
        this.issuccess = response ?  'Successfully applied' : 'Failed to apply';
        this._snackBar.open(this.issuccess, null, {
          duration: 2000,
        });
      });
  }

getfilteredJobService(value): Observable<jobcode[]> {
    return this.jobService.searchJobType(value).pipe(
      map((response: jobcode[]) => {
        return response;
      }),
      catchError(err => {
        return throwError(err);
      }));
  }

  displayJob(job): string {
    return job ? job.JobTitle : '';
  }

  displayLoc(loc: location): string {
    return loc ? loc.locationName : '';
  }

  get f() {
    return this.jobPostForm.controls;
  }

  onSubmit(){
    if (this.jobPostForm.invalid) {
      return;
    }

    this.jobDetails.getJobDetailsByType(this.f.worktype.value, this.f.location.value)
    .subscribe(response => { this.jobsResponse = response; });
  }

}
