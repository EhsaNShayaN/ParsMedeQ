import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {FormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {first} from 'rxjs/operators';
import {BaseResult} from '../../core/models/BaseResult';
import {BaseComponent} from '../../base-component';
import {ToastrService} from 'ngx-toastr';
import {Ticket, TicketResponse} from '../../core/models/TicketResponse';
import {Tables} from '../../core/constants/server.constants';
import {TicketService} from '../../core/services/rest/ticket-service';
import {AuthService} from '../../core/services/auth.service';
import {Helpers} from '../../core/helpers';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.scss'],
  standalone: false
})
export class TicketComponent extends BaseComponent implements OnInit {
  public contactForm?: UntypedFormGroup;
  public answerForm?: UntypedFormGroup;
  submitClick = false;
  submitted = false;
  error = '';
  ticket: Ticket | null = null;
  edit = false;
  file: any;
  answerFile: any;
  protected readonly Tables = Tables;

  constructor(private ticketService: TicketService,
              private authService: AuthService,
              private formBuilder: FormBuilder,
              private helpers: Helpers,
              private route: ActivatedRoute,
              private toaster: ToastrService) {
    super();
  }

  ngOnInit(): void {
    this.route.params.subscribe(({id}) => {
      if (id) {
        this.edit = true;
        this.ticketService.getTicket(id).subscribe((resp: TicketResponse) => {
          if (this.authService.isAdmin() && resp.data[0].status === 1) {
            this.ticketService.updateTicketStatus(id, 2).subscribe((r: BaseResult<boolean>) => {
              resp.data[0].status = 2;
              this.initForm(resp);
            });
          } else {
            this.initForm(resp);
          }
        });
      } else {
        this.contactForm = this.formBuilder.group({
          title: ['', Validators.required],
          description: ['', Validators.required],
          file: null
        });
      }
    });
  }

  initForm(resp: TicketResponse) {
    this.ticket = resp.data[0];
    this.ticket.statusText = this.helpers.getTicketStatus(this.ticket.status);
    this.answerForm = this.formBuilder.group({
      answer: ['', Validators.required],
      file: null
    });
  }

  handleFileInput(target: any) {
    if (target.files && target.files[0]) {
      this.file = target.files[0];
    }
  }

  handleAnswerFileInput(target: any) {
    if (target.files && target.files[0]) {
      this.answerFile = target.files[0];
    }
  }

  public onContactFormSubmit(values: any): void {
    if (this.contactForm?.valid) {
      this.ticketService.addTicket(values, this.file).subscribe((result: BaseResult<boolean>) => {
        this.toaster.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        this.contactForm!.reset();
      });
    }
  }

  public onAnswerFormSubmit(values: any): void {
    if (this.answerForm?.valid) {
      values.ticketId = this.ticket?.id;
      this.ticketService.addTicketAnswer(values, this.answerFile).pipe(first()).subscribe((resp: TicketResponse) => {
          this.ticket = null;
          this.submitted = false;
          this.submitClick = false;
          this.initForm(resp);
          this.toaster.success(this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL'), '', {});
        },
        error => {
          this.error = error;
          this.submitClick = false;
        });
    }
  }
}
