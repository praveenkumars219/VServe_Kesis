import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { JobdetailService } from 'src/app/services/jobdetail.service';
import { jobdetail } from 'src/app/models/jobdetail';

@Component({
  selector: 'app-homechild',
  templateUrl: './homechild.component.html',
  styleUrls: ['./homechild.component.css']
})
export class HomechildComponent implements OnInit {

  jobs: jobdetail[];

  constructor(
    private authService: AuthService,
    private jobDetails: JobdetailService
  ) { }

  ngOnInit(): void {
    this.getjobDetails();
  }

  getjobDetails() {
    this.jobDetails.getAllJobDetails().subscribe(
     response => { this.jobs = response; console.log(this.jobs); }
    );
  }

}
