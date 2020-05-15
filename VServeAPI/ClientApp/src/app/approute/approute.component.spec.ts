import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprouteComponent } from './approute.component';

describe('ApprouteComponent', () => {
  let component: ApprouteComponent;
  let fixture: ComponentFixture<ApprouteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApprouteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApprouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
