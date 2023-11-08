import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { DashboardService } from '../../dashboard.service';
import { IAddBillingInput } from '../../interfaces/add-billing.interface';
import { AlertComponent } from 'ngx-bootstrap/alert';

@Component({
  selector: 'app-billing-form',
  templateUrl: './billing-form.component.html',
  styleUrls: ['./billing-form.component.scss']
})
export class BillingFormComponent implements OnInit{
  form: FormGroup;

  alerts: any[] = [];

  constructor(public fb: FormBuilder, private dashboardService: DashboardService) {
    this.form = this.fb.group({
      value: ['', Validators.required],
      customerName: ['', Validators.required],
      customerEmail: ['', [Validators.required, Validators.email]]
    });

  }

  ngOnInit(): void {
  }

  onSubmit(formTemplate: FormGroupDirective): void {
    const { value, customerName, customerEmail} = this.form.value;
    const input: IAddBillingInput = {
      value,
      customerName,
      customerEmail
    }
    this.dashboardService.add(input).subscribe({
      next: (resp) => {
        if(resp?.data){
          formTemplate.resetForm();
          this.clearForm();
          this.addAlert("success", "Cobrança adicionada com sucesso!")
        } else {
          const mensagemErro = resp.messages;
          this.addAlert("danger", `Erro ao salvar: ${mensagemErro}`)
        }
      },
      error: () => {
        this.addAlert("danger", "Erro ao salvar!")
      }
    })
  }

  clearForm(): void {
    this.form.reset();
  }

  addAlert(type: string, message: string): void {
    this.alerts.push({
      type: type,
      msg: message,
      timeout: 5000
    });
  }

  onClosedAlert(dismissedAlert: AlertComponent): void {
    this.alerts = this.alerts.filter(alert => alert !== dismissedAlert);
  }

}
