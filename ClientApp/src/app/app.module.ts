import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './components/login/login.component';
import { AppRoutingModule } from './app-routing.module';
import { JwtokenInterceptor } from './helpers/jwtoken.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { RegisterComponent } from './components/register/register.component';
import { JobpostComponent } from './components/jobpost/jobpost.component';
import { JobsearchComponent } from './components/jobsearch/jobsearch.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppmaterialModule } from './appmaterial.module';
import { ProfileComponent } from './components/profile/profile.component';
import { DatePipe } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { PaymentsComponent } from './components/payments/payments.component';
import { PaymentsPipe } from './pipes/payments.pipe';
import { PayeefilterPipe } from './pipes/payeefilter.pipe';
import { NotificationComponent } from './components/notification/notification.component';
import { JobtitlefilterPipe } from './pipes/jobtitlefilter.pipe';
import { HomechildComponent } from './components/homechild/homechild.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegisterComponent,
    JobpostComponent,
    JobsearchComponent,
    ProfileComponent,
    PaymentsComponent,
    PaymentsPipe,
    PayeefilterPipe,
    NotificationComponent,
    JobtitlefilterPipe,
    HomechildComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AppmaterialModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    DatePipe,
    MatDatepickerModule,
    MatNativeDateModule  
  ],
  bootstrap: [AppComponent],
  exports: [PaymentsPipe, PayeefilterPipe, JobtitlefilterPipe]
})
export class AppModule { }
