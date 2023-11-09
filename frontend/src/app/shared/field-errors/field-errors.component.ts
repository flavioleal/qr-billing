import { Component, Host, Input, OnInit, SkipSelf } from '@angular/core';
import { AbstractControl, AbstractControlDirective, FormGroupDirective, ValidationErrors } from '@angular/forms';

export interface ValidationMessages {
  [key: string]: string | Function;
}

@Component({
  selector: 'app-field-errors',
  templateUrl: './field-errors.component.html',
  styleUrls: ['./field-errors.component.scss']
})
export class FieldErrorsComponent implements OnInit {
  messageList: ValidationMessages[] = [
    { required: 'Campo obrigatório.' },
    {
      minlength: (params: any) =>
        `O número mínimo de caracteres é ${params.requiredLength}`,
    },
    {
      maxlength: (params: any) =>
        'O número máximo permitido de caracteres é ' + params.requiredLength,
    },
    { pattern: 'Campo obrigatório' },
    {
      min: (params: any) =>
        `O valor mínimo é ${params.min.toString().replace('.', ',')}`,
    },
    { dateDiff: 'A data fim não pode ser menor que a data início.' },
    { email: 'O E-mail não esta em um formato válido' },
    { notSame: 'As senhas não conferem' },
    { notSameEmail: 'Não condiz com o email informado.' },
    { notValidNumeroDemanda: "Número inválido. (Nº/AAAA)" },
    { notValidSei: "Número SEI inválido." },
    { notValidPeriodo: "Obrigatório preencher uma data de início e fim." }
  ];
  @Input() control:
    | AbstractControlDirective
    | AbstractControl
    | undefined
    | null;
  @Input() nomeLabel: string = '';
  constructor(@Host() @SkipSelf() private form: FormGroupDirective) { }
  ngOnInit(): void { }
  shouldShowErrors(): boolean {
    if (
      this.control &&
      this.control?.errors &&
      (this.control?.dirty || this.control?.touched || this.form.submitted)
    )
      return true;
    else return false;
  }
  listOfErrors(): string[] {
    return Object.keys(
      (this.control as AbstractControlDirective | AbstractControl)
        ?.errors as ValidationErrors
    ).map((field) =>
      this.getMessage(field, (this.control?.errors as ValidationErrors)[field])
    );
  }
  private getMessage(type: string, params: any): string {
    const message = this.messageList.find((x) => x[type]);
    if (message === undefined) return '';
    const messageValue: string =
      message?.[type] instanceof Function
        ? (message?.[type] as Function)(params)
        : (message?.[type] as string);

    const messageFormatado = messageValue.replace('${nome}', this.nomeLabel);
    return messageFormatado;
  }
}
