import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AlertComponent } from 'ngx-bootstrap/alert';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { UserService } from 'src/app/core/services/user.service';

import { BillingService } from '../../billing.service';
import { IBillingFilter } from '../../interfaces/billing-filter.interface';
import { ICancelBilling } from '../../interfaces/cancel-billing.interface';
import { IListBillingOutput } from '../../interfaces/list-billing.interface';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  page?: number = 1;
  totalRecords: number = 0;

  listBilling: IListBillingOutput[] = [];

  isModalQrCodeOpen: boolean = false;
  dataModalQrCode?: IListBillingOutput;
  qrCode: string = "";
  alerts: any[] = [];
  formResearch: FormGroup;

  idBilling: string = '';

  isModalConfirmationOpen: boolean = false;

  roleUser: string = '';
  constructor(public fb: FormBuilder,
    private billingService: BillingService,
    private userService: UserService) {
    this.formResearch = this.fb.group({
      status: [''],
      merchantId: ['']
    });

    this.userService.$user.subscribe({
      next: (resp) => {
        if (resp?.role) {
          this.roleUser = resp.role;
        }
      }
    })
  }

  ngOnInit(): void {
    this.research();
  }

  research() {
    const { status, merchantId } = this.formResearch.value;
    const filter: IBillingFilter = {
      page: this.page,
      status: status,
      merchantId: merchantId
    }
    this.billingService.getByFilter(filter).subscribe({
      next: (resp) => {
        if (resp?.data) {
          this.totalRecords = resp.data?.totalRecords;
          this.listBilling = resp.data?.list;
        } else {
          this.totalRecords = 0;
          this.listBilling = [];
          this.addAlert("info", "Nenhum resultado encontrado!")
        }
      },
      error: (ex) => {
        this.totalRecords = 0;
        this.listBilling = [];
        if (ex.status == 400) {
          this.addAlert("danger", `Erro ao pesquisar: ${ex.error}`)
        } else {
          this.addAlert("danger", `Houve um erro interno na aplicação, por favor tente novamente mais tarde ou consulte o suporte`)
        }
      }
    });
  }

  pageChanged(event: PageChangedEvent): void {
    this.page = event.page;
    this.research();
  }

  openModalQrCode(record: IListBillingOutput) {
    this.dataModalQrCode = record;
    this.qrCode = record.qrCode;
    this.isModalQrCodeOpen = true;
  }

  closeModalQrCode() {
    this.isModalQrCodeOpen = false;
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



  openModalConfirmation(record: IListBillingOutput) {
    this.idBilling = record.id;
    this.isModalConfirmationOpen = true;
  }

  closeModalConfirmation() {
    this.clearIdBilling();
    this.isModalConfirmationOpen = false;
  }

  private clearIdBilling() {
    this.idBilling = '';
  }

  cancelBilling() {
    const input: ICancelBilling = {
      id: this.idBilling
    };
    this.billingService.cancel(this.idBilling, input).subscribe({
      next: (resp) => {
        if (resp?.data) {
          this.research();
          this.addAlert("success", "Cobrança cancelada com sucesso!")
          this.closeModalConfirmation();
        } else {
          const mensagemErro = resp.messages;
          this.addAlert("danger", `Erro ao cancelar: ${mensagemErro}`)
        }
      },
      error: (ex) => {
        if (ex.status == 400) {
          this.addAlert("danger", `Erro ao cancelar: ${ex.error}`)
        } else {
          this.addAlert("danger", `Houve um erro interno na aplicação, por favor tente novamente mais tarde ou consulte o suporte`)
        }
      }
    })
  }

}
