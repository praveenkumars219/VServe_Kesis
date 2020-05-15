import {
  Component,
  OnInit
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import {
  ActivatedRoute,
  Router
} from '@angular/router';
import {
  UserService
} from 'src/app/services/user.service';
import { first } from 'rxjs/operators';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) {

  }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required],
      confirmpassword: ['', Validators.required],
      displayname: ['', Validators.required]
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  onLogin() {
    this.router.navigate(['/login']);
  }

  onSubmit() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;
    const user: User = {
      email: this.f.email.value,
      password: this.f.password.value,
      confirmPassword: this.f.confirmpassword.value,
      displayName: this.f.displayname.value
    };
    this.userService.createuser(user).pipe(first())
    .subscribe(
      data => {
        this.router.navigate(['login']);
      },
      error => {
        this.error = error;
        this.loading = false;
      });

  }

}
