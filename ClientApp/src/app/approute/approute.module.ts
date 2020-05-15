import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ApprouteComponent } from './approute.component';


const routes: Routes = [
  { path: '', component: ApprouteComponent }
];

@NgModule({
  declarations: [ApprouteComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ApprouteModule { }
