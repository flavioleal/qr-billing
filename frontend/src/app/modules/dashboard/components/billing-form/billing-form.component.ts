import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { AlertComponent } from 'ngx-bootstrap/alert';

import { BillingService } from '../../billing.service';
import { IAddBillingInput } from '../../interfaces/add-billing.interface';

@Component({
  selector: 'app-billing-form',
  templateUrl: './billing-form.component.html',
  styleUrls: ['./billing-form.component.scss']
})
export class BillingFormComponent implements OnInit{
  form: FormGroup;

  alerts: any[] = [];

  constructor(public fb: FormBuilder, private billingService: BillingService) {
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
    this.billingService.add(input).subscribe({
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
      error: (ex) => {
        if (ex.status == 400) {
          this.addAlert("danger", `Erro ao salvar: ${ex.error}`)
        } else {
          this.addAlert("danger", `Houve um erro interno na aplicação, por favor tente novamente mais tarde ou consulte o suporte`)
        }
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
