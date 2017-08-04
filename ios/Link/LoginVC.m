//
//  LoginVC.m
//  Link
//
//  Created by Sergey on 4/21/16.
//  Copyright © 2016 Sergey. All rights reserved.
//

#import "LoginVC.h"

@interface LoginVC () <UITextFieldDelegate>
{
    CGRect  mainViewFrame;
    
    __weak IBOutlet UIView *mainView;
    
    __weak IBOutlet UITextField *userNameTF;
    __weak IBOutlet UITextField *passwordTF;
    __weak IBOutlet UIButton *logInButton;
    __weak IBOutlet UIButton *registrButton;
    
}
- (IBAction)logInButton:(UIButton *)sender;
- (IBAction)registrButton:(UIButton *)sender;

@end

@implementation LoginVC

#pragma mark LifeCicle

- (void)viewDidLoad {
    [super viewDidLoad];
    [self setView];
    [self setNavigation];
    [self setNotification];
}

-(void) viewDidAppear:(BOOL)animated {
    mainViewFrame = mainView.frame;
}

-(void) viewDidDisappear:(BOOL)animated {
    [self.view endEditing:YES];
}

-(void) dealloc {
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

#pragma mark Methods
-(void) setView {
    [userNameTF setPlaceholderColor];
    [passwordTF setPlaceholderColor];
}

-(void) setNavigation {
    [self.navigationController.navigationBar setTitleTextAttributes:@{NSForegroundColorAttributeName:[UIColor whiteColor]}];
    self.navigationController.navigationBar.tintColor = [UIColor whiteColor];
    [self.navigationController setNavigationBarHidden:YES];
}

-(void) setNotification {
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(keyboardWillShow:) name:UIKeyboardWillShowNotification object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(keyboardWillHide:) name:UIKeyboardWillHideNotification object:nil];
}

#pragma mark Actions
- (IBAction)logInButton:(UIButton *)sender {
    UIViewController * vc = [self.storyboard instantiateViewControllerWithIdentifier:@"SurveysVC"];
    [self.navigationController pushViewController:vc animated:YES];
}

- (IBAction)registrButton:(UIButton *)sender {
    UIViewController * vc = [self.storyboard instantiateViewControllerWithIdentifier:@"RegistrationVC"];
    [self.navigationController pushViewController:vc animated:YES];
}

#pragma mark UITextFieldDelegate
-(BOOL)textFieldShouldReturn:(UITextField *)textField
{
    if ([textField isEqual:userNameTF]) {
        [passwordTF becomeFirstResponder];
    }else if ([textField isEqual:passwordTF]) {
        [passwordTF resignFirstResponder];
    }
    return YES;
}

#pragma mark KeyboardNotification
- (void)keyboardWillShow:(NSNotification*)notification
{
    if (mainView.frame.origin.y == mainViewFrame.origin.y) {
        NSDictionary* keyboardInfo = [notification userInfo];
        NSValue* keyboardFrameBegin = [keyboardInfo valueForKey:UIKeyboardFrameBeginUserInfoKey];
        CGRect keyboardRect = [keyboardFrameBegin CGRectValue];
        CGRect bVRect = mainView.frame;
        mainView.frame = CGRectMake(bVRect.origin.x, bVRect.origin.y - keyboardRect.size.height + (bVRect.origin.y - 1), bVRect.size.width, bVRect.size.height);
    }
}

- (void)keyboardWillHide:(NSNotification*)notification
{
    NSDictionary* keyboardInfo = [notification userInfo];
    NSValue* keyboardFrameBegin = [keyboardInfo valueForKey:UIKeyboardFrameBeginUserInfoKey];
    CGRect keyboardRect = [keyboardFrameBegin CGRectValue];
    CGRect bVRect = mainView.frame;
    mainView.frame = CGRectMake(bVRect.origin.x, bVRect.origin.y + keyboardRect.size.height - (mainViewFrame.origin.y - 1), bVRect.size.width, bVRect.size.height);
}

@end

