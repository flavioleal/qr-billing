import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldErrorsComponent } from './field-errors.component';

describe('FieldErrorsComponent', () => {
  let component: FieldErrorsComponent;
  let fixture: ComponentFixture<FieldErrorsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FieldErrorsComponent]
    });
    fixture = TestBed.createComponent(FieldErrorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
